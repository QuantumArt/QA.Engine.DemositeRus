ARG NODE_IMAGE=node:16
ARG RUNTIME_IMAGE=${NODE_IMAGE}-slim

FROM ${NODE_IMAGE} as build
WORKDIR /app

COPY package*.json ./

RUN npm ci

COPY .browserslistrc ./
COPY angular.json ./
COPY server.ts ./
COPY tsconfig*.json ./
COPY src ./src

RUN npm run build:ssr -- --configuration=production

FROM $RUNTIME_IMAGE as runtime
RUN apt-get update && apt-get install -y --no-install-recommends dumb-init

WORKDIR /app

COPY --from=build /app/node_modules ./node_modules
COPY --from=build /app/package*.json ./
COPY --from=build /app/dist ./dist

ENV NODE_ENV production
EXPOSE 4000

USER node
CMD ["dumb-init", "node", "dist/demosite/server/main.js"]