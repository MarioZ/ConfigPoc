This project is a proof of concept for achieving reusing (or better said inheriting) of configuration values in custom section.  
Any configuration element can define a value that references its existing values and/or values that any of its parent configuration elements have.

For example consider the following custom configuration section:
```xml
<userSection application="FOO"
             version="1.0">
  <user name="$(firstName) $(lastName)"
        firstName="John"
        lastName="Doe"
        welcomeMessage="Hello $(name), welcome to $(application) v$(version)!" />
</userSettings>
```
The result of *`welcomeMessage`* value is:
```
Hello John Doe, welcome to FOO v1.0!
```
**TODO list:**
- [x] Add support for inheriting value from ConfigurationElement.
- [x] Add support for inheriting value from ConfigurationElementCollection.
- [x] Add support for inheriting value from ConfigurationSection.
- [x] Add support for Standard and Custom Numeric Format Strings.  
      *Implementation: `$(placeholderName \# #,##0.00)`*
- [x] Add support for Standard and Custom Date and Time Format Strings.  
      *Implementation: `$(placeholderName \@ MMMM dd, yyyy)`*
- [ ] Add support for inheriting value from ConfigurationSectionGroup.
- [ ] Add support for inheriting value from ConfigurationSectionGroupCollection.
- [ ] **Feel free to contact me for any additional suggestions you may have!**