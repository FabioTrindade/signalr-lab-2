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
