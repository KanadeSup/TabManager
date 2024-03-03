import { authRoutes } from "./authRoutes";
import { spaceRoutes } from "./spaceRoutes";

export const routes = [
   ...authRoutes,
   ...spaceRoutes
];