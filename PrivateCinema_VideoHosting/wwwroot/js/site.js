
var connection = new signalR.HubConnectionBuilder().withUrl("/cinemaHub").build();

var currentVideoPath = null;
var selectedVideo = null; 
var currentUser = null;

connection.on("InitCurrentUser", function (user) {
    currentUser = user;
});

connection.on("RecieveMovieList", function (movieList) {
    var movieListWindow = document.getElementById("filmItemList");
    movieListWindow.innerHTML = "";
    movieList.forEach(addVideoItem);
    const scrollingElement = document.getElementsByClassName()[0];
    scrollingElement.scrollTop = scrollingElement.scrollHeight;
});
connection.on("Refresh", function () {
    connection.invoke("RefreshMovieList", user.userIdentifier);
});
function addVideoItem(video) {
    var li = document.createElement("li");
    var button = document.createElement("button");

    button.className = "group-connected";
    button.innerHTML = `${video.fileName}`;

    button.onclick = function (event) {
        selectedVideo = video;
        currentVideoPath = video.filePath;
        var filmHeader = document.getElementById("chosenFilm");
        filmHeader.textContent = `${selectedVideo.fileName}`;
        connection.invoke("GetValidPathSource", currentVideoPath);
    };
    li.appendChild(button);
    document.getElementById("filmItemList").appendChild(li);
}
connection.on("InitSource", function (validPath) {
    console.log(validPath)
    console.log("string is received")
    var sourceItem = getElementById("videoSource");
    var playerItem = getElementById("video-player");
    sourceItem.scr = validPath;
    playerItem.load();
    playerItem.play()
});

document.getElementById("uploadFile").addEventListener("click", () => {

    fetch("upload", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            "text": message,
            "connectionid": connectionId
        })
    }).catch(error => console.error(error));
});




connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});



