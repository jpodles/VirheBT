using AutoMapper;

using NUnit.Framework;

using System;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Services.Implementations;
using VirheBT.Services.Interfaces;
using VirheBT.Shared.DTOs;

namespace VirheBT.Tests
{
    public class IssueServiceTests
    {
        public IIssueService MockIssueService { get; set; }

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Issue, IssueDto>());
            MockIssueService = new IssueService(new MockIssueRepo(),
                                                new MockIssueCommentRepo(),
                                                new MockAppUserRepo(),
                                                new MockIssueHistoryRepo(),
                                                new Mapper(config));
        }

        [Test]
        public void GetIssueHistory_InvalidIssueId_ShouldThrowException()
        {
            int? issueId = null;
            Assert.ThrowsAsync<ArgumentException>(
                async () => await MockIssueService.GetIssueHistory(issueId));
        }

        [Test]
        public void AddComment_NullComment_ShouldThrowException()
        {
            CreateCommentDto commentDto = null;
            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await MockIssueService.AddCommentAsync(commentDto));
        }

        [Test]
        public void AddIssue_InvalidIssue_ShouldThrowException()
        {
            CreateIssueDto issue = null;
            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await MockIssueService.AddIssueAsync(123, issue));
        }

        [Test]
        public void InvalidIssueIdIssueHistory()
        {
            int? issueId = null;
            Assert.ThrowsAsync<ArgumentException>(
                async () => await MockIssueService.GetIssueHistory(issueId));
        }

        [Test]
        public void DeleteComment_InvalidIssueId_ShouldThrowException()
        {
            int? issueId = null;
            Assert.ThrowsAsync<ArgumentException>(
                async () => await MockIssueService.DeleteCommentAsync(issueId, 123));
        }

        [Test]
        public void DeleteComment_InvalidCommentId_ShouldThrowException()
        {
            int? commentId = null;
            Assert.ThrowsAsync<ArgumentException>(
                async () => await MockIssueService.DeleteCommentAsync(123, commentId));
        }

        [Test]
        public void DeleteIssue_InvalidIssueId_ShouldThrowException()
        {
            int? issueId = null;
            Assert.ThrowsAsync<ArgumentException>(
                async () => await MockIssueService.DeleteIssueAsync(123, issueId));
        }

        [Test]
        public void DeleteIssue_InvalidProjectId_ShouldThrowException()
        {
            int? projectId = null;
            Assert.ThrowsAsync<ArgumentException>(
                async () => await MockIssueService.DeleteIssueAsync(projectId, 123));
        }

        [Test]
        public void EditComment_InvalidComment_ShouldThrowException()
        {
            EditCommentDto editCommentDto = null;
            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await MockIssueService.EditCommentAsync(editCommentDto));
        }

        [Test]
        public void GetIssue_InvalidProjectId_ShouldThrowException()
        {
            int? projectId = null;
            Assert.ThrowsAsync<ArgumentException>(
              async () => await MockIssueService.GetIssueAsync(projectId, 123));
        }

        [Test]
        public void GetIssue_InvalidIssueId_ShouldThrowException()
        {
            int? issueId = null;
            Assert.ThrowsAsync<ArgumentException>(
              async () => await MockIssueService.GetIssueAsync(123, issueId));
        }

        [Test]
        public void GetIsssues_InvalidProjectId_ShouldThrowException()
        {
            int? projectId = null;
            Assert.ThrowsAsync<ArgumentException>(
              async () => await MockIssueService.GetIssuesAsync(projectId));
        }
    }
}