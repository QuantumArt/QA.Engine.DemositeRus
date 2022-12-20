ARG NODE_IMAGE=node:16
ARG RUNTIME_IMAGE=${NODE_IMAGE}-slim

FROM ${NODE_IMAGE} as build
WORKDIR /app

COPY package*.json ./

RUN npm install

COPY .browserslistrc ./
COPY angular.json ./
COPY server.ts ./
COPY tsconfig*.json ./
COPY src ./src

RUN npm run build:ssr -- --configuration=production

FROM $RUNTIME_IMAGE as runtime
WORKDIR /app

COPY --from=build /app/node_modules ./node_modules
COPY --from=build /app/package*.json ./
COPY --from=build /app/dist ./dist

CMD ["node", "dist/demosite/server/main.js"]
