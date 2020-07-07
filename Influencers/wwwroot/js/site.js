// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Upvote/Downvote functionality

var script = document.createElement('script');
script.src = 'https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js';
document.getElementsByTagName('head')[0].appendChild(script);

var script2 = document.createElement('script2');
script2.src = 'https://cdn.jsdelivr.net/npm/js-cookie@2/src/js.cookie.min.js';
document.getElementsByTagName('head')[0].appendChild(script2);

id = -1;
flag = -1;

function setValues(id, flag) {
    this.id = id;
    this.flag = flag;

    if (flag == 1) {
        handleUpvote();
    }

    if (flag == 2) {
        handleDownvote();
    }
}

function handleUpvote() {

    let currentArticle = Cookies.get(id);
    let counter = $(`#article-counter-${id}`);

    if (currentArticle) {
        if (currentArticle == "1") { //handle upvote
      
            Cookies.remove(id);
            counter.text(`${+counter.text() - 1}`);
            $(`#article-up-${id}`).css("color", "dimgray");
            $(`#article-down-${id}`).css("color", "dimgray");
            sendVote(id, -1);
        } else if (currentArticle == "0"){
            //upvote after downvote
            flag = 1;
            Cookies.set(id, flag);
            counter.text(`${+counter.text() + 2}`);
            $(`#article-up-${id}`).css("color", "#3CBC8D");
            $(`#article-down-${id}`).css("color", "dimgray");
            sendVote(id, 2);
            
        }
    } else {
        //1st time upvote
        Cookies.set(id, 1);
        counter.text(`${+counter.text() + 1}`);
        $(`#article-up-${id}`).css("color", "#3CBC8D");
        $(`#article-down-${id}`).css("color", "dimgray");
        sendVote(id, 1)
    }
}

function handleDownvote() {
    let currentArticle = Cookies.get(id);
    let counter = $(`#article-counter-${id}`);

    if (currentArticle) {
        if (currentArticle == "0") { //handle downvote
            Cookies.remove(id)
            counter.text(`${+counter.text() + 1}`);
            $(`#article-up-${id}`).css("color", "dimgray");
            $(`#article-down-${id}`).css("color", "dimgray");
            sendVote(id, 1);
        } else if(currentArticle == "1"){
            //downvote after upvote
            flag = 0;
            Cookies.set(id, flag);
            counter.text(`${+counter.text() - 2}`);
            $(`#article-up-${id}`).css("color", "dimgray");
            $(`#article-down-${id}`).css("color", "#3CBC8D");
            sendVote(id, -2);

        }
    } else {
        //1st time downvote
        flag = 0;
        Cookies.set(id, flag);
        counter.text(`${+counter.text() - 1}`);
        $(`#article-up-${id}`).css("color", "dimgray");
        $(`#article-down-${id}`).css("color", "#3CBC8D");
        sendVote(id, -1)
    }
}

function sendVote(id, flag) {
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

//upvote/downvote view article

function colorArticleVotes(articleId) {

    if (Cookies.get(articleId) == "1") {
        $(`#article-up-${articleId}`).css("color", "#3CBC8D");
        $(`#article-down-${articleId}`).css("color", "dimgray");
    }

    if (Cookies.get(articleId) == "0") {
        $(`#article-up-${articleId}`).css("color", "dimgray");
        $(`#article-down-${articleId}`).css("color", "#3CBC8D");
    }

}

//comment upvote/downvote functionality 

function setCommentValues(id, flag) {
    this.id = id;
    this.flag = flag;

    if (flag == 1) {
        handleCommentUpvote();
    }

    if (flag == 2) {
        handleCommentDownvote();
    }
}

function handleCommentUpvote() {

    let currentComment = Cookies.get(id);
    let counter = $(`#comment-counter-${id}`);

    if (currentComment) {
        if (currentComment == "1") { //handle upvote

            Cookies.remove(id);
            counter.text(`${+counter.text() - 1}`);
            $(`#comment-up-${id}`).css("color", "dimgray");
            $(`#comment-down-${id}`).css("color", "dimgray");
            sendCommentVote(id, -1);
        } else if (currentComment == "0") {
            //upvote after downvote
            flag = 1;
            Cookies.set(id, flag);
            counter.text(`${+counter.text() + 2}`);
            $(`#comment-up-${id}`).css("color", "#3CBC8D");
            $(`#comment-down-${id}`).css("color", "dimgray");
            sendCommentVote(id, 2);

        }
    } else {
        //1st time upvote
        Cookies.set(id, 1);
        counter.text(`${+counter.text() + 1}`);
        $(`#comment-up-${id}`).css("color", "#3CBC8D");
        $(`#comment-down-${id}`).css("color", "dimgray");
        sendCommentVote(id, 1)
    }
}

function handleCommentDownvote() {
    let currentComment = Cookies.get(id);
    let counter = $(`#comment-counter-${id}`);

    if (currentComment) {
        if (currentComment == "0") { //handle downvote
            Cookies.remove(id)
            counter.text(`${+counter.text() + 1}`);
            $(`#comment-up-${id}`).css("color", "dimgray");
            $(`#comment-down-${id}`).css("color", "dimgray");
            sendCommentVote(id, 1);
        } else if (currentComment == "1") {
            //downvote after upvote
            flag = 0;
            Cookies.set(id, flag);
            counter.text(`${+counter.text() - 2}`);
            $(`#comment-up-${id}`).css("color", "dimgray");
            $(`#comment-down-${id}`).css("color", "#3CBC8D");
            sendCommentVote(id, -2);

        }
    } else {
        //1st time downvote
        flag = 0;
        Cookies.set(id, flag);
        counter.text(`${+counter.text() - 1}`);
        $(`#comment-up-${id}`).css("color", "dimgray");
        $(`#comment-down-${id}`).css("color", "#3CBC8D");
        sendCommentVote(id, -1)
    }
}

function sendCommentVote(id, flag) {
    $.ajax({
        type: 'POST',
        url: 'http://localhost:44379/api/Voting/AddCommentVote',
        contentType: 'application/json',
        data: JSON.stringify({
            Flag: flag,
            CommentId: id
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