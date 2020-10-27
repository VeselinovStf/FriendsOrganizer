using FriendsOrganizer.Data.Models;
using System;

namespace FriendsOrganizer.UI.ModelsWrappers
{
    public class MeetingModelWrapper : ModelWrapperBase<Meeting>
    {
        public MeetingModelWrapper(Meeting model) : base(model)
        {
        }

        public int Id { get { return GetValue<int>(); } }


        public string Title
        {
            get { return GetValue<string>(); }
            set { SetValue<string>(value); }
        }

        public DateTime StartAt
        {
            get { return GetValue<DateTime>(); }
            set 
            {
                SetValue<DateTime>(value);
                if (StartAt < EndAt)
                {
                    EndAt = StartAt;
                }
            }
        }

        public DateTime EndAt
        {
            get { return GetValue<DateTime>(); }
            set
            {
                SetValue<DateTime>(value);
                if (EndAt < StartAt)
                {
                    StartAt = EndAt;
                }
            }
        }

    }
}
