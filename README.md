This project is a proof-of-concept (POC) for a read-only **app.config** elements inheritance in a custom configuration section.

The goal was to achieve reusing of existing configuration properties inside another configuration property; any configuration element should be able to define placeholders in its property's value that would reference its existing properties and/or properties that any of its parent configuration elements have.

The idea was inspired by MSBuild's properties and items placeholders (e.g., **$(Platform)** and **@(Compile)** placeholders).

For example, consider the following custom configuration section:

```xml
<UserSettings Application="MY APP"
              Version="1.0">
  <User FullName="$(FirstName) $(LastName)"
        FirstName="John"
        LastName="Doe"
        WelcomeMessage="Hello $(FullName), welcome to $(Application) v$(Version)!" />
</UserSettings>
```

The result of `WelcomeMessage` property is:

```
Hello John Doe, welcome to MY APP v1.0!
```

**Notes:**
- ☑︎ Added support for inherited values from `ConfigurationElement`.
- ☑︎ Added support for inherited values from `ConfigurationElementCollection`.
- ☑︎ Added support for inherited values from `ConfigurationSection`.
- ☑︎ Added support for global values from `appSettings`.
- ☑︎ Added support for Standard and Custom Numeric Format, e.g. `$(placeholderName \# #,##0.00)`.
- ☑︎ Added support for Standard and Custom Date and Time Format, e.g. `$(placeholderName \@ MMMM dd, yyyy)`.