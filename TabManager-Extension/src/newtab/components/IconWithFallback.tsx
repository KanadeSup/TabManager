import { Avatar } from "@mantine/core";
import { IconBrowser } from "@tabler/icons-react";

export function IconWithFallback({ src, size }: { src?: string, size?: number}) {
   return (
      <Avatar src={src} size={size} variant="transparent">
         <IconBrowser />
      </Avatar>
   );
}