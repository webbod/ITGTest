// visual studio 2013 lints against an early spec of javascript 
// and doesn't recognise arrow functions or string interpolation

var currentPage = 0;

var keepOpen = "keepOpen";
var accordion = "accordion";

var mode = keepOpen;

var articleTemplate = (article) => {
    var output = `<div class="article">
        <div class="article-date" data-year="${article.Year}" data-month="${article.Month}">${article.Day}</div>
        <div class="article-headline">${article.Headline}</div>
        <div class="article-image" style="background-image:url(${article.ImageUrl});"></div>
        <div class="article-summary">${article.Summary}</div>
        <div class="article-body">${article.Body}</div>
    </div>`;

    return output;
}

var bindExpandableElements = () => {
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

var loadMoreArticles = () => {
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

var init = () => {
    $("#loadMore").on("click", () => {loadMoreArticles(); });
    loadMoreArticles();
}


$(document ).ready(() => {
    init();
});

 