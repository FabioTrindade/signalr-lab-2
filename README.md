# signalr-lab-2
Novo lab do signalr com controle de grupo

## Database
### Redis
- Utilizado para controle de conexão

| Key | Value | Descrição |
| --- | ----- | --------- | 
| Analista | [...] | Lista com Id's das conexões do analista |
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

![signalr-lab-2](https://user-images.githubusercontent.com/30089341/236485684-7bee9898-e9b9-4a47-b5ba-73dd49f32224.png)
