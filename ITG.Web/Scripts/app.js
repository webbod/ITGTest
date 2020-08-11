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
    var output = `<article class="article">
        <time class="article-date" datetime="${article.Year}-${article.Month}-${article.Day}" data-year="${article.Year}" data-month="${article.Month}">${article.Day}</time>
        <div class="article-headline">${article.Headline}</div>
        <div class="article-image" style="background-image:url(${article.ImageUrl});"></div>
        <div class="article-summary">${article.Summary}</div>
        <div class="article-body">${article.Body}</div>
    </article>`;

    return output;
}

// binds a click handler to expand/collapse articles
const bindExpandableElements = () => {
    $(".article")
    .not(".expandable")
    .addClass("expandable")
    .each((index, element) => {
        $(element).on("click", (e) =>  { toggleExpanded(e) });
    });
}

// expands and collapses articles
// mode = { keepOpen | accordion } controls the behaviour
const toggleExpanded = (evt) => {
    var el = evt.currentTarget;

    if(mode === "keepOpen"){
        $(el).toggleClass("expanded");
    }
    else {
        $(".expanded").removeClass("expanded");
        $(el).addClass("expanded");
    }
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
    $("#loadMore").on("click", () => { loadMoreArticles(); });
    loadMoreArticles();
}


// initalises the page on 
$(document).ready(() => {
    init();
});

 