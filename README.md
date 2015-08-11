BizTalk<Extended\>
======================
_"Mjolnir"_ is an open-source project that will take your BizTalk development to the next level.

# Features at a glance
BizTalk<Extended\> offers the following features:

- Typed interaction with the message context

## Planned features
- Generic pipeline component

For a full list, have a look at the `feature`-issues [here](labels/feature).

# Usage
## Interacting with message context
### "Add reference to BizTalk DLL"

> C:\Program Files (x86)\Microsoft BizTalk Server 2013 R2\Microsoft.BizTalk.GlobalPropertySchemas.dll

### Reading

	var action = message.ReadContextProperty<WCF.Action, string>();

TBW

	var action = message.ReadContextProperty<WCF.Action, string>(isMandatory: false);

### Writing & Promoting

	message.WriteContextProperty<WCF.Action>("Send");

TBW

	message.PromoteContextProperty<WCF.Action>("Send");

# Requirements
In order to use this library you should meet these requirements:

- BizTalk 2013 R2 or higher
- Visual Studio 2013+
- .Net Framework 4.5+

# License
This project is licensed under the [MIT license](license).


![Codit Logo](assets/codit_logo.png)