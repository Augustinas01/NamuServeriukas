import ServerControlTile from "./ServerControlTile";
import settings from "../appsettings.json";
import { useState,useEffect } from 'react';

function ServerControlGrid() {
    let [data, setData]= useState([]);
    useEffect(() => {
        fetch(settings.API.GAMES + '/list')
            .then((response) => response.json())
            .then((data) => setData(data));

    },[]);
    return (
        <div className="container mt-3">
            <div className="tile is-ancestor">
                <div className="tile is-parent">
                    {data.map(game => <ServerControlTile key={game.id} name={game.name} state={game.state} /> ) }

                </div>
            </div>
        </div>
    )
}



export default ServerControlGrid