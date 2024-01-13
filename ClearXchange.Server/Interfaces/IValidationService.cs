﻿using ClearXchange.Server.Model;

namespace ClearXchange.Server.Interfaces
{
    public interface IValidationService
    {
        void ValidateMember(Member member);
        void ValidateID(string Id);
        void ValidateName(string Name);
        void ValidateDateOfBirth(DateTime DateOfBirth);
        void ValidateEmail(string Email);
        void ValidatePhone(string Phone);

    }

}