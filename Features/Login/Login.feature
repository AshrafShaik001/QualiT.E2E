@smoke @login
Feature: Login
  As a registered user
  I want to sign in to the application
  So that I can access my workspace

  Background:
    Given the user is on the login page

  @TC-CL-01
  Scenario: Login to application using valid credentials
    When the user signs in as an admin
    Then the user is logged in
