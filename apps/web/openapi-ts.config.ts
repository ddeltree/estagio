import { defineConfig } from "@hey-api/openapi-ts";

export default defineConfig({
  input: "http://localhost:5291/swagger/v1/swagger.json",
  output: "src/api",
});
