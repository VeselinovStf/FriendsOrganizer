using FriendsOrganizer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FriendsOrganizer.Data.Configuration
{
    public class FriendMeetingConfiguration : IEntityTypeConfiguration<FriendMeeting>
    {
        public void Configure(EntityTypeBuilder<FriendMeeting> builder)
        {
            builder.HasKey(fm => new { fm.MeetingId, fm.FriendId });

            builder.HasOne(fm => fm.Friend)
                .WithMany(f => f.FriendMeetings)
                .HasForeignKey(f => f.FriendId);

            builder.HasOne(fm => fm.Meeting)
                .WithMany(m => m.FriendMeetings)
                .HasForeignKey(m => m.MeetingId);
        }
    }
}
