export function base64ImgToImgData(base64Img: string) {
   const imageType = base64Img.split(";")[0].split(":")[1];
   const byteString = base64Img.split(",")[1];
   const byteCharacters = window.atob(byteString);
   const byteArrays = [];
   for (let i = 0; i < byteCharacters.length; i++) {
      byteArrays.push(byteCharacters.charCodeAt(i));
   }
   const byteArray = new Uint8Array(byteArrays);
   const bytes = new Blob([byteArray], { type: imageType });
   return {
      type: imageType,
      data: bytes
   }   
}