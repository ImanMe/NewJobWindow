﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JobWindowNew.API.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class JobWindowContext : DbContext
    {
        public JobWindowContext()
            : base("name=JobWindowContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ApplicantApstream> ApplicantApstreams { get; set; }
    
        public virtual ObjectResult<sp_BeyondFeed_Result> sp_BeyondFeed()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_BeyondFeed_Result>("sp_BeyondFeed");
        }
    
        public virtual ObjectResult<sp_CareerBuilder_Result> sp_CareerBuilder()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_CareerBuilder_Result>("sp_CareerBuilder");
        }
    
        public virtual ObjectResult<sp_Craigslist_Result> sp_Craigslist()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Craigslist_Result>("sp_Craigslist");
        }
    
        public virtual ObjectResult<sp_GlassdoorFeed_Result> sp_GlassdoorFeed()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GlassdoorFeed_Result>("sp_GlassdoorFeed");
        }
    
        public virtual ObjectResult<sp_IndeedFeed_Result> sp_IndeedFeed()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_IndeedFeed_Result>("sp_IndeedFeed");
        }
    
        public virtual ObjectResult<sp_JobingFeed_Result> sp_JobingFeed()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_JobingFeed_Result>("sp_JobingFeed");
        }
    
        public virtual ObjectResult<sp_Jobs2CareersFeed_Result> sp_Jobs2CareersFeed()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Jobs2CareersFeed_Result>("sp_Jobs2CareersFeed");
        }
    
        public virtual ObjectResult<sp_JobWindow_Result> sp_JobWindow()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_JobWindow_Result>("sp_JobWindow");
        }
    
        public virtual ObjectResult<sp_Monster_Result> sp_Monster()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Monster_Result>("sp_Monster");
        }
    
        public virtual ObjectResult<sp_SnagaJobFeed_Result> sp_SnagaJobFeed()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_SnagaJobFeed_Result>("sp_SnagaJobFeed");
        }
    
        public virtual ObjectResult<sp_TweetMyJobs_Result> sp_TweetMyJobs()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_TweetMyJobs_Result>("sp_TweetMyJobs");
        }
    
        public virtual ObjectResult<sp_TweetMyJobsOA_Result> sp_TweetMyJobsOA()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_TweetMyJobsOA_Result>("sp_TweetMyJobsOA");
        }
    
        public virtual ObjectResult<sp_ZipRecruiterFeed_Result> sp_ZipRecruiterFeed()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ZipRecruiterFeed_Result>("sp_ZipRecruiterFeed");
        }
    }
}
