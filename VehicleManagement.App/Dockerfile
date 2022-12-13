### Build ###
FROM node:18-alpine3.15 AS build
ARG ENV
WORKDIR /usr/src/app
COPY package.json package-lock.json ./
RUN npm install
COPY . .

FROM build as test
RUN npm run lint:ci
RUN npm run test:ci

FROM build as publish
RUN npm run build-$ENV

FROM scratch AS export-test
COPY --from=test /testresults/junit/unit-test-result.xml /testresults
COPY --from=test /testresults/coverage/code-coverage.xml /testresults

FROM scratch AS export-app
ARG ENV
COPY --from=publish /usr/src/app/dist/$ENV /app
