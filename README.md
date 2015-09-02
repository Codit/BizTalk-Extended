BizTalk\<Extended\>
======================
[![Join the chat at https://gitter.im/CoditEU/Mjolnir](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/CoditEU/Mjolnir?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

_BizTalk\<Extended\>_ is an open-source project that will take your BizTalk development to the next level.

# Features at a glance
BizTalk<Extended\> offers the following features:

- Typed interaction with the message context

# Documentation
## Typed interaction with message context
Fixed strings in code are evil and should be avoided at all times when possible to prevent typos, especially when interacting with the message context.

We provide you the tools to specify the type of your property and handle it all for you. You can use your own custom property schemas or existing BizTalk schemas.

To interact with these BizTalk schemas you'll need to reference `Microsoft.BizTalk.GlobalPropertySchemas.dll` that contains all the types.
You can find it here:
> C:\Program Files (x86)\Microsoft BizTalk Server 2013 R2\Microsoft.BizTalk.GlobalPropertySchemas.dll


### Writing & Promoting to the context

	message.WriteContextProperty<WCF.Action>("Send");

TBW

	message.PromoteContextProperty<WCF.Action>("Send");

It also works with your custom property schemas. Here we will write *Codito* as `CompanyName` in our `Customer` schema.

	message.PromoteContextProperty<Customer.CompanyName>("Codito");

When we look at the tracking you see that it automagically retrieves the namespace and writes the value to the context
![Writing to the context](media/docs-writing-to-context.png)

You're not limited to strings, you can also pass in enumerations and we'll handle it!

	message.PromoteContextProperty<Customer.SupportPlan>(SupportPlan.FirstLine);

### Reading from the context
You can read values from the context as well - You simply specify the property you're interested in and what type of value you are expecting it to be.

	string action = message.ReadContextProperty<WCF.Action, string>();

By default all properties are mandatory. This means that when a context property is not found in the context a `ContextPropertyNotFoundException` will be thrown containing the *Name* & *Namespace* of the property.

However, if you only want to read an property if it is present you can mark it as optional. This allows you to read the value if it's present and otherwise receive `null` and no exception will be thrown.

	string action = message.ReadContextProperty<WCF.Action, string>(isMandatory: false);

> **Remark** - Value-types will return their default value when the specified property is not present. If you want to receive a `null` you'll need to mark the expected type as a nullable type i.e. `int?`.

# Planned features
- Generic pipeline component

For a full list of the planned features, have a look at the `feature`-issues [here](https://github.com/CoditEU/Mjolnir/labels/feature).

# Requirements
In order to use this library you should meet these requirements:

- BizTalk 2013 R2 or higher
- Visual Studio 2013 or higher
- .Net Framework 4.5 or higher

# License
This project is licensed under the [MIT license](LICENSE).


![Codit Logo](assets/codit_logo.png)