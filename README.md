# Smokeball Dev Project

An desktop application that connects to custom google result scrapper to determine the ranking for a given website.

**Development Enviornment used:**
1) VS2019
2) .Net Core 3.1

**Requirement**: 
CEO wants to know how many times their company is listed on google search result for the search keyword "conveyancing software"

**Project is divided in different layers:**

**Smokeball.API:** API to centralize the logic to process the request and provide the result with the help of Domain layer. This layer also validate the input request. Swagger is been provided to give the insight of the API. Currently, its been consumed in Desktop application, but later will create Web application that will also consume the same API, this reduces the core code repeatation. 

**Smokeball.Domain:** Main core logic of scrapping and providing the ranking result. HtmlDownloader can be used to implement any html download for a given website.

**Smokeball.Common:** Holds request/response & common models and extension methods.

**Smokeball.Desktop:** WPF application to show the ranking and occurances, along with the HTML search result preview which is provided in next tab to make sure the listing is correct.

**Smokeball.UnitTest:** MS Unit testing project

**Note** 
1) Before running the WPF application please change the log4Net output path in Desktop project App.Config file
2) Before running Unit test project please change the htmlMockFilePath in SmokeballService.cs file to your local location
