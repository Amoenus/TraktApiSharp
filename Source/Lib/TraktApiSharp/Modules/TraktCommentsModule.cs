﻿namespace TraktApiSharp.Modules
{
    using Extensions;
    using Objects.Basic;
    using Objects.Get.Movies;
    using Objects.Get.Shows;
    using Objects.Get.Shows.Episodes;
    using Objects.Get.Shows.Seasons;
    using Objects.Get.Users.Lists;
    using Objects.Post.Comments;
    using Objects.Post.Comments.Responses;
    using Requests;
    using Requests.WithOAuth.Comments;
    using Requests.WithoutOAuth.Comments;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class TraktCommentsModule : TraktBaseModule
    {
        public TraktCommentsModule(TraktClient client) : base(client) { }

        public async Task<TraktComment> GetCommentAsync(string id)
        {
            ValidateId(id);

            return await QueryAsync(new TraktCommentSummaryRequest(Client) { Id = id });
        }

        public async Task<TraktListResult<TraktComment>> GetCommentsAsync(string[] ids)
        {
            if (ids == null || ids.Length <= 0)
                return null;

            var comments = new List<TraktComment>(ids.Length);

            for (int i = 0; i < ids.Length; i++)
            {
                var show = await GetCommentAsync(ids[i]);

                if (show != null)
                    comments.Add(show);
            }

            return new TraktListResult<TraktComment> { Items = comments };
        }

        public async Task<TraktCommentPostResponse> PostMovieCommentAsync(TraktMovie movie, string comment,
                                                                          bool? spoiler = null, TraktSharing sharing = null)
        {
            ValidateComment(comment);

            return await QueryAsync(new TraktCommentPostRequest<TraktMovieCommentPost>(Client)
            {
                RequestBody = new TraktMovieCommentPost
                {
                    Movie = movie,
                    Comment = comment,
                    Spoiler = spoiler,
                    Sharing = sharing
                }
            });
        }

        public async Task<TraktCommentPostResponse> PostShowCommentAsync(TraktShow show, string comment,
                                                                         bool spoiler = false, TraktSharing sharing = null)
        {
            ValidateComment(comment);

            return await QueryAsync(new TraktCommentPostRequest<TraktShowCommentPost>(Client)
            {
                RequestBody = new TraktShowCommentPost
                {
                    Show = new TraktShow
                    {
                        Title = show.Title,
                        Ids = show.Ids
                    },
                    Comment = comment,
                    Spoiler = spoiler,
                    Sharing = sharing
                }
            });
        }

        public async Task<TraktCommentPostResponse> PostSeasonCommentAsync(TraktSeason season, string comment,
                                                                           bool spoiler = false, TraktSharing sharing = null)
        {
            ValidateComment(comment);

            return await QueryAsync(new TraktCommentPostRequest<TraktSeasonCommentPost>(Client)
            {
                RequestBody = new TraktSeasonCommentPost
                {
                    Season = new TraktSeason
                    {
                        Ids = season.Ids
                    },
                    Comment = comment,
                    Spoiler = spoiler,
                    Sharing = sharing
                }
            });
        }

        public async Task<TraktCommentPostResponse> PostEpisodeCommentAsync(TraktEpisode episode, string comment,
                                                                            bool spoiler = false, TraktSharing sharing = null)
        {
            ValidateComment(comment);

            return await QueryAsync(new TraktCommentPostRequest<TraktEpisodeCommentPost>(Client)
            {
                RequestBody = new TraktEpisodeCommentPost
                {
                    Episode = new TraktEpisode
                    {
                        Ids = episode.Ids
                    },
                    Comment = comment,
                    Spoiler = spoiler,
                    Sharing = sharing
                }
            });
        }

        public async Task<TraktCommentPostResponse> PostListCommentAsync(TraktList list, string comment,
                                                                         bool spoiler = false, TraktSharing sharing = null)
        {
            ValidateComment(comment);

            return await QueryAsync(new TraktCommentPostRequest<TraktListCommentPost>(Client)
            {
                RequestBody = new TraktListCommentPost
                {
                    List = new TraktList
                    {
                        Ids = list.Ids
                    },
                    Comment = comment,
                    Spoiler = spoiler,
                    Sharing = sharing
                }
            });
        }

        public async Task<TraktCommentPostResponse> UpdateCommentAsync(string commentId, string comment, bool spoiler = false)
        {
            ValidateId(commentId);
            ValidateComment(comment);

            return await QueryAsync(new TraktCommentUpdateRequest(Client)
            {
                Id = commentId,
                RequestBody = new TraktCommentUpdatePost
                {
                    Comment = commentId,
                    Spoiler = spoiler
                }
            });
        }

        public async Task<TraktCommentPostResponse> PostCommentReplyAsync(string commentId, string comment, bool spoiler = false)
        {
            ValidateId(commentId);
            ValidateComment(comment);

            return await QueryAsync(new TraktCommentReplyRequest(Client)
            {
                Id = commentId,
                RequestBody = new TraktCommentReplyPost
                {
                    Comment = comment,
                    Spoiler = spoiler
                }
            });
        }

        public async Task DeleteCommentAsync(string commentId)
        {
            ValidateId(commentId);

            await QueryAsync(new TraktCommentDeleteRequest(Client) { Id = commentId });
        }

        public async Task LikeCommentAsync(string commentId)
        {
            ValidateId(commentId);

            await QueryAsync(new TraktCommentLikeRequest(Client) { Id = commentId });
        }

        public async Task UnlikeCommentAsync(string commentId)
        {
            ValidateId(commentId);

            await QueryAsync(new TraktCommentUnlikeRequest(Client) { Id = commentId });
        }

        public async Task<TraktComment> GetCommentReplyAsync(string replyId)
        {
            ValidateId(replyId);

            return await QueryAsync(new TraktCommentReplySummaryRequest(Client) { Id = replyId });
        }

        public async Task<TraktPaginationListResult<TraktComment>> GetCommentRepliesAsync(string id, int? page = null, int? limit = null)
        {
            ValidateId(id);

            return await QueryAsync(new TraktCommentRepliesRequest(Client)
            {
                Id = id,
                PaginationOptions = new TraktPaginationOptions(page, limit)
            });
        }

        private void ValidateId(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("comment id not valid", "id");
        }

        private void ValidateComment(string comment)
        {
            if (string.IsNullOrEmpty(comment))
                throw new ArgumentException("comment is empty", "comment");

            if (comment.WordCount() < 5)
                throw new ArgumentException("comment has too few words - at least five words are required", "comment");
        }
    }
}
