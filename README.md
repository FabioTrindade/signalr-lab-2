# signalr-lab-2
Novo lab do signalr com controle de grupo, utilizando redis

![signalr-lab-2](https://github.com/FabioTrindade/signalr-lab-2/assets/30089341/0cfdd0a0-9410-429e-b948-7124b320a0c6)

## Database
### Redis
- Utilizado para controle de conexão

| Key | Value | Descrição |
| --- | ----- | --------- | 
| Analista | Grupo | Nome do grupo selecionado |
| Grupo | [...] | Lista com Id's das conexões no grupo |

## Docker
``` sh
docker compose build
...
docker compose up -d
```

## K8s
```sh
kubectl apply -f k8s/signalr.yaml
```
