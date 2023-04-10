import settings from '../appsettings.json';
import { useState, useEffect } from 'react';
import { DownOutlined } from '@ant-design/icons';


function ServerControlTile({ name, state, serviceId }) {
    let [tileData, setTileData] = useState({
        serviceId: serviceId,
        name: name,
        state: state,
        online: state == 'online' ? true : false,
        requested: false,
        requestedFor: '',
        loaded: false,
        startTime: '',
        playerList: []
    });
    let [intervalMS, setIntervalMS] = useState(10);
    let [showPlayers, setShowPlayers] = useState(false);
    let [rotation, setRotation] = useState('0');

    useEffect(() => {

        const intervalId = setInterval(() => {
            fetch(settings.API.SERVICE + '/info/' + serviceId)
                .then((response) => response.json())
                .then(data => {
                    setTileData({
                        ...tileData,
                        state: data.state,
                        online: data.state == 'online' ? true : false,
                        requested: getRequestedState(tileData, data),
                        startTime: new Date(data.startTime),
                        playerList: data.playerList
                    });
                })
                .catch(e => {
                    setTileData({
                        ...tileData,
                        state: "offline",
                        online: false,
                        requested: false,
                        startTime: null,
                        playerList: []
                    })
                });
        }, intervalMS);
        changeServerButtonAppearance(tileData);

        return () => clearInterval(intervalId);
    }, [tileData]);

    useEffect(() => {
        if (tileData.requested) {
            setIntervalMS(100);
        } else {
            setIntervalMS(1000);
        }
    }, [tileData.requested]);



    return (
        <article className="tile is-child is-3" >
            <div className="card">
                <div className="card-header is-shadowless is-justify-content-center">
                    <p className="card-header-title"> {tileData.name} </p>
                    <p className="block is-justify-content-flex-end is-size-7 p-4" > {tileData.state} </p>
                </div>
                <hr className="my-0 mx-2"></hr>
                <CardContent tileData={tileData} setShowPlayers={setShowPlayers} showPlayers={showPlayers} rotation={rotation} setRotation={setRotation} />
                <footer className="card-footer">
                    <CardButton tileData={tileData} setTileData={setTileData} />
                </footer>
            </div>
        </article>
    )
}

function CardContent({ tileData, setShowPlayers, showPlayers, rotation, setRotation }) {
    function showPlayersBtn_Click() {
        setShowPlayers(!showPlayers);
        showPlayers ? setRotation('0') : setRotation('90');
    }

    return (
        <div className="card-content p-4">
            <div className="content mx-0 has-text-weight-medium">
                {tileData.online ?
                    <div>
                        <p className=" mt-0 mb-0">
                            Online since: <span className=" has-text-weight-bold">{tileData.startTime.toLocaleString('lt-lt')}</span>
                        </p>
                        <div className="is-flex">
                            <p className=" mt-0 mb-0">
                                Players online:
                                <span className="mx-2 has-text-weight-bold">{tileData.playerList.length}</span>
                            </p>
                            {tileData.playerList.length > 0 ?
                                <div className="ml-1 is-clickable" onClick={showPlayersBtn_Click}>
                                    <DownOutlined rotate={rotation}  style={{ fontSize: '0.9rem', weight: '1rem' }} />
                                </div>
                                : null
                            }
                        </div>
                        <div hidden={showPlayers ? false : true}>
                            <table className="table is-size-7 has-text-centered ">
                                <thead>
                                    <tr>
                                        <th>Player</th>
                                        <th>Joined</th>
                                    </tr>
                                </thead>
                                <tbody id="playerTableBody">
                                    {tileData.playerList.map((player) => (
                                        <tr key={player.name}>
                                            <td>{player.name}</td>
                                            <td>{new Date(player.joinTimestamp).toLocaleTimeString('lt-lt')}</td>
                                        </tr>
                                    )) }
                                </tbody>
                            </table>
                        </div>

                    </div>
                    :
                    null
                }
            </div>

        </div>
    )
}


function CardButton({ tileData, setTileData }) {

    const serverButton_Click = () => {
        setTileData({
            ...tileData,
            requested: true,
            requestedFor: tileData.online ? 'offline' : 'online'
        });
        document.getElementById("serverBtn").classList.add("is-loading");

        if (tileData.online) {
            stop(tileData.serviceId);
        } else {
            start(tileData.serviceId);
        }
        changeServerButtonAppearance(tileData);
    }

    return (
        <button
            id="serverBtn"
            className="card-footer-item button is-radiusless"
            onClick={serverButton_Click}
            disabled={tileData.playerList.length > 0 ? true : false}>
        {tileData.online ? 'Stop' : 'Start'}
        </button>
    )
}


function start(serviceId) {
    fetch(settings.API.SERVICE + '/start/' + serviceId)
        .then((response) => {
            if (!response.ok) {
                alert('error');
                return
            }
        })
        .catch(e => console.log(e));
};

function stop(serviceId) {
    fetch(settings.API.SERVICE + '/stop/' + serviceId)
        .then((response) => {
            if (!response.ok) {
                alert('error');
                return
            }
        })
        .catch(e => console.log(e));
};


function changeServerButtonAppearance(tileData) {

    if (!tileData.requested) {
        document.getElementById("serverBtn").classList.remove("is-loading");
    } else {
        document.getElementById("serverBtn").classList.add("is-loading");
    }
    if (tileData.state == 'online') {
        document.getElementById("serverBtn").classList.remove('is-success');
        document.getElementById("serverBtn").classList.add('is-danger');
    } else {
        document.getElementById("serverBtn").classList.remove('is-danger');
        document.getElementById("serverBtn").classList.add('is-success');
    }
}

function getRequestedState(tileData, data) {
    if (tileData.requestedFor != '' && tileData.requested && tileData.requestedFor != data.state) {
            return true;
    } else {
        return false;
    }
    return false;
}



export default ServerControlTile