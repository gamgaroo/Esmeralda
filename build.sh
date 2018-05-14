set -ex

dotnet restore
dotnet build

dotnet publish ./src/Gamgaroo.Esmeralda.App -c Release -o ./bin/Docker

docker build -t $USERNAME/$IMAGE:latest ./src/Gamgaroo.Esmeralda.App --no-cache