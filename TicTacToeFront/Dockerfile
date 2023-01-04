FROM node:alpine AS app-build
WORKDIR /app
COPY . .
RUN npm ci && npm run build

FROM nginx:alpine
COPY ./default.conf /etc/nginx/conf.d/default.conf
COPY --from=app-build /app/dist/tictactoe /usr/share/nginx/html
EXPOSE 3000