// constants
var keepOpen = "keepOpen";
var accordion = "accordion";

// global variables - but only within the context of this page
var currentPage = 0;
var mode = keepOpen;

//  renders an article in HTML
const articleTemplate = (article) => {
    var output = `<article class="article" role="article">
        <time class="article-date" datetime="${article.year}-${article.month}-${article.day}" data-year="${article.year}" data-month="${article.month}">${article.day}</time>
        <div class="article-headline" role="heading">${article.headline}</div>
        <img src="/images/empty.png" class="article-image" style="background-image:url(${article.imageUrl});" title="${article.summary}" role="img"/>
        <div class="article-summary">${article.summary}</div>
        <div class="article-body">${article.body}</div>
    </article>`;

    return output;
}

// binds a click handler to expand/collapse articles
const bindExpandableElements = () => {
    $(".article")
        .not(".expandable")
        .addClass("expandable")
        .each((index, element) => {
            $(element).on("click", (e) => { toggleExpanded(e) });
        });
}

// expands and collapses articles
// mode = { keepOpen | accordion } controls the behaviour
const toggleExpanded = (evt) => {
    var el = evt.currentTarget;

    if (mode === "keepOpen") {
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
        if (data !== null && data.length > 0) {
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

