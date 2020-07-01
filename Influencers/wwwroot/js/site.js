// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Upvote/Downvote functionality

var script = document.createElement('script');
script.src = 'https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js';
document.getElementsByTagName('head')[0].appendChild(script); 

const up_vote_spans = document.getElementsByClassName('up-vote');
const down_vote_spans = document.getElementsByClassName('down-vote');
const count = document.getElementsByClassName('number');

let votes = [];

for (let i = 0; i < count.length; i += 1) {
    const thisUpVoteSpan = up_vote_spans[i];
    const thisDownVoteSpan = down_vote_spans[i];
    votes[i] = { up: false, down: false };

    thisUpVoteSpan.addEventListener('click', handleUpvote.bind(null, i), false);
    thisDownVoteSpan.addEventListener('click', handleDownvote.bind(null, i), false);
}

function handleUpvote(i) {
    const currentVote = votes[i];
    const matchingUpSpan = up_vote_spans[i];
    const matchingDownSpan = down_vote_spans[i];
    const matchingCount = count[i];
    const currentCount = parseInt(matchingCount.innerHTML);

    if (currentVote.down) {
        matchingCount.innerHTML = currentCount + 2;
    } else if (currentVote.up === false) {
        matchingCount.innerHTML = currentCount + 1;
    } else {
        matchingCount.innerHTML = currentCount - 1;
    }
    if (!currentVote.up) {
        matchingUpSpan.style.color = "#3CBC8D";
        matchingDownSpan.style.color = 'dimgray';
        currentVote.up = true;
        currentVote.down = false;
    } else {
        matchingUpSpan.style.color = 'dimgray';
        currentVote.up = false;
    }
}

function handleDownvote(i) {
    const currentVote = votes[i];
    const matchingUpSpan = up_vote_spans[i];
    const matchingDownSpan = down_vote_spans[i];
    const matchingCount = count[i];
    const currentCount = parseInt(matchingCount.innerHTML);

    if (currentVote.up) {
        matchingCount.innerHTML = currentCount - 2;
    } else if (currentVote.down === false) {
        matchingCount.innerHTML = currentCount - 1;
    } else {
        matchingCount.innerHTML = currentCount + 1;
    }
    if (!currentVote.down) {
        matchingDownSpan.style.color = "#3CBC8D";
        matchingUpSpan.style.color = 'dimgray';
        currentVote.down = true;
        currentVote.up = false;
    } else {
        matchingDownSpan.style.color = 'dimgray';
        currentVote.down = false;
    }
}

const sendVote = (id, flag) => {

    $.ajax({
        type: 'POST',
        url: 'http://localhost:44379/api/Voting/AddVote',
        contentType: 'application/json',
        data: JSON.stringify({
            Flag: flag,
            ArticleId: id
        }),
        error: function (err) {
            //$('#info').html('<p>An error has occurred</p>');
            console.log(err);
        },
        success: function (data) {
            console.log(data)
            //var $title = $('<h1>').text(data.talks[0].talk_title);
            //var $description = $('<p>').text(data.talks[0].talk_description);
            //$('#info')
            //    .append($title)
            //    .append($description);
        }
    });
}