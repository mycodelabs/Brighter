﻿using FakeItEasy;
using Machine.Specifications;
using Paramore.Domain.DomainServices;
using Paramore.Domain.Entities.Meetings;
using Paramore.Domain.Factories;
using Paramore.Domain.ValueTypes;

namespace Paramore.Tests.domain.Meetings
{
    [Subject("The policy for booking tickets")]
    public class OverbookingPolicyTests
    {
        static FiftyPercentOverbookingPolicy bookingPolicy;
        static IIssueTickets ticketIssuer;

        Establish context = () =>
        {
            ticketIssuer = A.Fake<IIssueTickets>();
            A.CallTo(() => ticketIssuer.Issue(new Capacity(150))).Returns(new Tickets(new Capacity(150)));    
            bookingPolicy = new FiftyPercentOverbookingPolicy(ticketIssuer);
        };

        Because of = () => bookingPolicy.AllocateTickets(new Capacity(100));

        It should_increase_the_tickets_by_50_percent_of_capacity = () => A.CallTo(() => ticketIssuer.Issue(new Capacity(150))).MustHaveHappened();
    }
}
