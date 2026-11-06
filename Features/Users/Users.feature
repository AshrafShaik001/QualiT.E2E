@smoke @users
Feature: Users
  As an administrator
  I want to manage users
  So that I can control who has access

  Background:
    Given the user is signed in as an admin

  @TC-CL-02
  Scenario: Navigate to users page and verify page view
    When the user opens the Users page
    Then the Users page is displayed
    And the user management actions and filters are available
