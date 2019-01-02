# WCF Testbed

This solution serves as a testbed for WCF extensibility.

See https://blogs.msdn.microsoft.com/carlosfigueira/2011/03/14/wcf-extensibility/


# Steps

1. Create an empty web application
2. Reference UrlRoutingModule in web.config
3. Add basic Service and ServiceDefinition classes
4. Add dependency injection and routing
5. Add one or more Services
6. Add an AuthorizationPolicy
7. Add an ErrorHandler
8. Add declarative authorization checks (primarily for role-based authorization)
9. Add method authorization checks (primarily for resource authorization)
