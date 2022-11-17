using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace ApiAssessmentProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var client = new RestClient("http://localhost:3000/fixtures");
            var request = new RestRequest(Method.GET);
            var response = client.Execute<List<Response>>(request);
            //var statusCode = (int)response.StatusCode;

            Assert.AreEqual(200, Convert.ToInt32(response.StatusCode));
            Assert.IsNotNull(response.Data.Count);
            Assert.AreEqual(1, Int32.Parse(response.Data[0].fixtureId));
            Assert.AreEqual(2, Convert.ToInt32(response.Data[1].fixtureId));
            Assert.AreEqual(3, Int32.Parse(response.Data[2].fixtureId));
        }

        [TestMethod]
        public void PostNewFixture()
        {
            var client = new RestClient("http://localhost:3000/fixture");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(new Response()
            {
                fixtureId = "4",
                fixtureStatus = new FixtureStatus()
                {
                    displayed = false,
                    suspended = true
                },
                footballFullState = new FootballFullState()
                {
                    homeTeam = "Manchester United",
                    awayTeam = "Chelsea",
                    finished = false,
                    gameTimeInSeconds = 2456,
                    goals = new List<Goal>()
                    {
                         new Goal
                         {
                              clockTime = 640,
                              confirmed = true,
                              id = 10,
                              ownGoal = false,
                              penalty = false,
                              period = "FIRST_HALF",
                              playerId = 10,
                              teamId = "1"
                         },
                             new Goal
                         {
                              clockTime = 840,
                              confirmed = true,
                              id = 20,
                              ownGoal = false,
                              penalty = false,
                              period = "FIRST_HALF",
                              playerId = 20,
                              teamId = "2"
                         },
                             new Goal
                         {
                              clockTime = 940,
                              confirmed = true,
                              id = 10,
                              ownGoal = false,
                              penalty = false,
                              period = "FIRST_HALF",
                              playerId = 10,
                              teamId = "1",
                         }
                    },
                    period = "SECOND_HALF",
                    possibles = new List<object>() { },
                    corners = new List<object>() { },
                    redCards = new List<object>() { },
                    yellowCards = new List<object>() { },
                    startDateTime = DateTime.Now,
                    started = true,
                    teams = new List<Team>()
                    {
                        new Team
                        {
                            association = "Home",
                            name = "Manchester United",
                            teamId = "Home",
                        },
                         new Team
                        {
                            association = "Away",
                            name = "Chelsea",
                            teamId = "Away"
                        }
                    }
                }
            });
            var response = client.Execute<List<Response>>(request);
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [TestMethod]
        public void GetNewlyAddedFixture()
        {
            var client = new RestClient("http://localhost:3000/fixture/4");
            var request = new RestRequest(Method.GET);

            var response = client.Execute<List<Response>>(request);
            if (response.IsSuccessful.Equals(true))
            {
                Assert.AreEqual(200, (int)response.StatusCode);
            }
        }

        [TestMethod]
        public void DeleteNewlyAddedFixture()
        {
            var client = new RestClient("http://localhost:3000/fixture/4");
            var request = new RestRequest(Method.DELETE);

            var response = client.Execute(request);
            if (response.IsSuccessful == true)
            {
                Assert.AreEqual(200, (int)response.StatusCode);
            }
            Console.WriteLine(response.Content);
        }
    }
}