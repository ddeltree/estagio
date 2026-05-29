FROM node:alpine AS builder

WORKDIR /usr/src/app

COPY . /usr/src/app

RUN npm install -g pnpm @angular/cli

RUN pnpm install

RUN pnpm --filter=web run build

FROM nginx:alpine

COPY --from=builder /usr/src/app/apps/web/dist/web/browser /usr/share/nginx/html

EXPOSE 80
