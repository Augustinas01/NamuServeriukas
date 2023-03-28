import settings from "./appsettings.json"

function ApiService() {
    GetGamesListOnServer();
}

function GetGamesListOnServer() {
    fetch(settings.API.GAMES + "list")
        .then((response) => response.json())
        .then((data) => {
            return data;
        })
}



export { ApiService }