using AutoMapper;

using NUnit.Framework;

using System;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Services.Implementations;
using VirheBT.Services.Interfaces;
using VirheBT.Shared.DTOs;

namespace VirheBT.Tests
{
    public class Tests
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
        public void InvalidIssueIdGetIssueHistory()
        {
            int? issueId = null;
            Assert.ThrowsAsync<ArgumentException>(
                async () => await MockIssueService.GetIssueHistory(issueId));
        }

        [Test]
        public void NullCommentAddComment()
        {
            CreateCommentDto commentDto = null;
            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await MockIssueService.AddCommentAsync(commentDto));
        }

        [Test]
        public void InvalidAddIssue()
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
        public void InvalidIssueIdDeleteComment()
        {
            int? issueId = null;
            Assert.ThrowsAsync<ArgumentException>(
                async () => await MockIssueService.DeleteCommentAsync(issueId, 123));
        }

        [Test]
        public void InvalidCommentIdDeleteComment()
        {
            int? commentId = null;
            Assert.ThrowsAsync<ArgumentException>(
                async () => await MockIssueService.DeleteCommentAsync(123, commentId));
        }

        [Test]
        public void InvalidIssueIdDeleteIssue()
        {
            int? issueId = null;
            Assert.ThrowsAsync<ArgumentException>(
                async () => await MockIssueService.DeleteIssueAsync(123, issueId));
        }

        [Test]
        public void InvalidCommentIdDeleteIssue()
        {
            int? projectId = null;
            Assert.ThrowsAsync<ArgumentException>(
                async () => await MockIssueService.DeleteIssueAsync(projectId, 123));
        }

        [Test]
        public void NullEditComment()
        {
            EditCommentDto editCommentDto = null;
            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await MockIssueService.EditCommentAsync(editCommentDto));
        }

        [Test]
        public void InvalidProjectIdGetIssue()
        {
            int? projectId = null;
            Assert.ThrowsAsync<ArgumentException>(
              async () => await MockIssueService.GetIssueAsync(projectId, 123));
        }

        [Test]
        public void InvalidIssueIdGetIssue()
        {
            int? issueId = null;
            Assert.ThrowsAsync<ArgumentException>(
              async () => await MockIssueService.GetIssueAsync(123, issueId));
        }

        [Test]
        public void InvalidProjectIdGetIsssues()
        {
            int? projectId = null;
            Assert.ThrowsAsync<ArgumentException>(
              async () => await MockIssueService.GetIssuesAsync(projectId));
        }
    }
}