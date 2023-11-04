On Windows:

    dotnet dev-certs https -ep https.pfx -p $CREDENTIAL_PLACEHOLDER$ --trust

On WSL2:

    dotnet dev-certs https --clean --import <<path-to-pfx>> --password $CREDENTIAL_PLACEHOLDER$

Where `$CREDENTIAL_PLACEHOLDER$` is a password.

Copy from Windows to Container:

    docker cp https.pfx 2acf72ea2e78:/workspaces/OpenGraphTilemaker/https.pfx
