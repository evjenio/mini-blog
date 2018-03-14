using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Threading.Tasks;
using MiniBlog.Contract;
using MvvmCross.Core.ViewModels;
using Serilog;
using IBlogService = MiniBlog.ServiceClient.Integration.IBlogService;

namespace MiniBlog.UI.Core.ViewModels
{
    /// <summary>
    /// Blog viewModel.
    /// </summary>
    public class BlogViewModel : MvxViewModel
    {
        private readonly IBlogService blogService;
        private readonly ILogger logger;

        private readonly MvxAsyncCommand<int> switchToArticleCommand;
        private string articleText;
        private byte[] picture;
        private ArticlePreviewDto selectedArticlePreview;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="blogService">blog service</param>
        /// <param name="logger">logger</param>
        public BlogViewModel(IBlogService blogService, ILogger logger)
        {
            this.blogService = blogService;
            this.logger = logger;

            switchToArticleCommand = new MvxAsyncCommand<int>(id => SwitchToArticle(id));
        }

        /// <summary>
        /// Article previews.
        /// </summary>
        public ObservableCollection<ArticlePreviewDto> Articles { get; set; } = new ObservableCollection<ArticlePreviewDto>();

        /// <summary>
        /// Comments.
        /// </summary>
        public ObservableCollection<CommentDto> Comments { get; set; } = new ObservableCollection<CommentDto>();

        /// <summary>
        /// Article text.
        /// </summary>
        public string ArticleText
        {
            get => articleText;
            set
            {
                articleText = value;
                RaisePropertyChanged(() => ArticleText);
            }
        }

        /// <summary>
        /// Image
        /// </summary>
        public byte[] Picture
        {
            get => picture;
            set
            {
                picture = value;
                RaisePropertyChanged(() => Picture);
            }
        }

        /// <summary>
        /// Selected Article preview.
        /// </summary>
        public ArticlePreviewDto SelectedArticlePreview
        {
            get => selectedArticlePreview;
            set
            {
                selectedArticlePreview = value;
                switchToArticleCommand.Execute(selectedArticlePreview.Id);
                RaisePropertyChanged(() => SelectedArticlePreview);
            }
        }

        /// <inheritdoc/>
        public override async Task Initialize()
        {
            Articles.Clear();
            ArticlePreviewDto[] articles = await LoadArticles();

            if (articles == null)
            {
                return;
            }

            foreach (var a in articles)
            {
                Articles.Add(a);
            }
        }

        private async Task SwitchToArticle(int id)
        {
            var article = await LoadArticle(id);
            if (article == null) return;

            ArticleText = article.Content;
            Picture = article.Image;
            Comments.Clear();

            if (article.Comments == null) return;

            foreach (var comment in article.Comments)
            {
                Comments.Add(comment);
            }
        }

        private async Task<ArticleDto> LoadArticle(int id)
        {
            try
            {
                return await blogService.GetArticleAsync(id);
            }
            catch (FaultException e)
            {
                logger.Error(e, "Error while invoking blogService");
                // log and suppress
                // TODO: Add notification via messenger
            }

            return null;
        }

        private async Task<ArticlePreviewDto[]> LoadArticles()
        {
            try
            {
                return await blogService.GetArticlePreviewsAsync();
            }
            catch (FaultException e)
            {
                logger.Error(e, "Error while invoking blogService");
                // log and suppress
                // TODO: Add notification via messenger
            }

            return null;
        }
    }
}
