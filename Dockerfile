FROM mcr.microsoft.com/dotnet/core/sdk:2.2.203-alpine3.9 AS build
LABEL maintainer="zhangcheng5468@gmail.com"
LABEL version="1.0"
WORKDIR /app

# copy
COPY ./. ./Renamer/
WORKDIR /app/Renamer/

# restore and publish
RUN dotnet restore && dotnet publish -c Release -o publish && rm publish/*.pdb 

FROM mcr.microsoft.com/dotnet/core/runtime:2.2.4-alpine3.9 AS runtime
WORKDIR /app
COPY --from=build /app/Renamer/publish ./

RUN mkdir root
ENV REPLACE_FROM ""
ENV REPLACE_TO ""
ENV RECURSION "-n"

ENTRYPOINT dotnet ColinChang.Renamer.dll "/app/root" $RECURSION $REPLACE_FROM $REPLACE_TO