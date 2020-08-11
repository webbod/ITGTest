// visual studio 2013 lints against an early spec of javascript 
// and doesn't recognise arrow functions or string interpolation

// I am using jQuery because it was fairly standard with MV4


// constants
var keepOpen = "keepOpen";
var accordion = "accordion";

// global variables - but only within the context of this page
var currentPage = 0;
var mode = keepOpen;

//  renders an article in HTML
const articleTemplate = (article) => {
    var output = `<div class="article">
        <div class="article-date" data-year="${article.Year}" data-month="${article.Month}">${article.Day}</div>
        <div class="article-headline">${article.Headline}</div>
        <div class="article-image" style="background-image:url(${article.ImageUrl});"></div>
        <div class="article-summary">${article.Summary}</div>
        <div class="article-body">${article.Body}</div>
    </div>`;

    return output;
}

// binds a click handler to expand/collapse articles
// mode = { keepOpen | accordion } controls the behaviour
const bindExpandableElements = () => {
    $(".article").not(".expandable")
    .addClass("expandable")
    .each((index, element) => {
        $(element).on("click", function(){
            if(mode === "keepOpen"){
                $(this).toggleClass("expanded");
            }
            else {
                $(".expanded").removeClass("expanded");
                $(this).addClass("expanded");
            }
        });
    });
}

// loads in a new set of articles and updates the UI
const loadMoreArticles = () => {
    var url = `/NewsFeed/Page/${currentPage}`;
    
    $("#loadMore").addClass("hidden");
    $("#loading").removeClass("hidden");
    
    $.getJSON(url, (data) => {
        $("#loading").addClass("hidden");
        if(data !== null && data.length > 0){
            $.each(data, (index, article) => {
                var html = articleTemplate(article);
                $("#articleContainer").append(html);
            });
            
            currentPage++;
            bindExpandableElements();
            $("#loadMore").removeClass("hidden");
            $("#loadMore")[0].scrollIntoView();
        } else {
            $("#allArticlesLoaded").removeClass("hidden");
        }
    });
}

// configures the UI and loads the first batch of articles
var init = () => {
    $("#loadMore").on("click", () => {loadMoreArticles(); });
    loadMoreArticles();
}


// initalises the page on 
$(document).ready(() => {
    init();
});

 