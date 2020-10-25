using FriendsOrganizer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FriendsOrganizer.Data.Configuration
{
    public class FriendConfiguration : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .IsRequired();

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(f => f.ProgrammingLanguage);

            builder.HasMany(f => f.PhoneNumbers)
                .WithOne(ffn => ffn.Friend)
                .HasForeignKey(ffn => ffn.FriendId);

            builder.HasMany(f => f.FriendMeetings)
                .WithOne(m => m.Friend)
                .HasForeignKey(m => m.FriendId);
        }
    }
}
