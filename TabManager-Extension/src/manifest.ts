import crypto from 'crypto'
import type PkgType from '../package.json'
import { isDev, port, r, preambleCode } from '../scripts/utils'
import fs from 'fs-extra'
import type { Manifest } from 'webextension-polyfill'

export async function getManifest() {
  const pkg = (await fs.readJSON(r('package.json'))) as typeof PkgType

  // update this file to update this manifest.json
  // can also be conditional based on your need
  const manifest: Manifest.WebExtensionManifest = {
    manifest_version: 2,
    name: pkg.displayName || pkg.name,
    version: pkg.version,
    description: pkg.description,
    icons: {
      16: './assets/icon-512.png',
      48: './assets/icon-512.png',
      128: './assets/icon-512.png',
    },
    permissions: ['tabs', 'storage', 'activeTab', 'http://*/', 'https://*/'],
    web_accessible_resources: ['dist/contentScripts/style.css'],
    chrome_url_overrides: {newtab: "./dist/newtab/index.html"},
  }

  if (isDev) {
    // for content script, as browsers will cache them for each reload,
    // we use a background script to always inject the latest version
    // see src/background/contentScriptHMR.ts
    delete manifest.content_scripts
    manifest.permissions?.push('webNavigation')

    const preambleCodeHash = crypto
      .createHash('sha256')
      .update(preambleCode)
      .digest('base64')

    // this is required on dev for Vite script to load
    manifest.content_security_policy = `script-src \'self\' 'sha256-${preambleCodeHash}' http://localhost:${port}; object-src \'self\'`
  }

  return manifest
}
