# ratings-api

## Setup Ingress controller
- Run command: `kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.1.2/deploy/static/provider/cloud/deploy.yaml`
- Wait 2 minutes then run: `kubectl annotate ingressClass nginx ingressclass.kubernetes.io/is-default-class=true -n nginx`

## Deploy to Kubernetes
- Run command for every file in k8s-manifests (don't forget about mongo folder) folder: `kubectl apply -f file.yaml`

## Check Kubernetes resources
- Run `kubectl get pods`
- Run `kubectl get deployments`
- Run `kubectl get services`
- Run `kubectl get ingress`

## Dependencies:
1. MongoDB
2. Docker/Kubernetes

## Run MongoDB in Docker:
`docker run --name some-mongo -p 27017:27017 -d mongo:latest`

## Build RatingsAPI Docker Image:
`docker build -f src/RatingsAPI.Host/Dockerfile -t ratings-api:1.0 .`

## Run RatingsAPI in Docker:
`docker run -p 5050:80 -e ASPNETCORE_ENVIRONMENT=Docker ratings-api:1.0`
