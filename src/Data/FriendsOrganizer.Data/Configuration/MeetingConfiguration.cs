using FriendsOrganizer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FriendsOrganizer.Data.Configuration
{
    public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> builder)
        {
            builder.HasMany(m => m.FriendMeetings)
             .WithOne(f => f.Meeting)
             .HasForeignKey(f => f.MeetingId);
        }
    }
}
