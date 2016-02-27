This project is a proof of concept for achieving reusing (or better said inheriting) of configuration values in custom section.  
Any configuration element can define a value that references its existing values and/or values that any of its parent configuration elements have.

For example consider the following custom configuration section:
```xml
<userSettings firstName="John"
              lastName="Doe"
              fullName="Mr. $(firstName) $(lastName)">
</userSettings>
```
The result of *`fullName`* value is:
```
Mr. John Doe
```
**Ideas:**
- [ ] Add support for inheriting from ConfigurationSectionGroup
- [ ] Add support for Standard and Custom Numeric Format Strings  
      *Suggestion: `$(placeholderName \# #,##0.00)`*
- [ ] Add support for Standard and Custom Date and Time Format Strings  
      *Suggestion: `$(placeholderName \@ MMMM dd, yyyy)`*
