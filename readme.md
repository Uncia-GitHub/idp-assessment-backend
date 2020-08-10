# IDP Candidate Challenge

This is a simple challenge designed to assess your test writing skills. It is intended to be done offline at your own speed using the tools of your choice. Please be prepared to present your solution when we meet. 
>Note there is no single correct solution - we are equally interested in your approach and thought process.

### Rules
1. You are free to use whatever tools you prefer.
2. You can use as much time as you like, but the intention is not to have you spend more than a couple of hours.
3. You are free to pull in any framework or libraries - be prepared to reason about your choices.
4. Use the patterns and practices you think fit best - again, be prepared to reason about your choices.
5. Commit your changes as you solve the various challenges and whenever you feel is a good time to commit. Let the commit message describe what part you solved.
6. Once done, zip the solution and send it back.

## Challenge 1

### Background information
We would like to create a UriValidator class ([URI](https://en.wikipedia.org/wiki/Uniform_Resource_Identifier)). Its `Validate` method should accept a `string uri` and a `IEnumerable<string> rules` of rules to validate. Return value should be a `bool` indicating the success of the validation. 

Below is a description of the various rules that can be passed to the validator class

-   absolute uri rules: 
	- example input: `https://app.confirmit.com/auth/logincallback`. 
	- Input has to match the exact value, otherwise it will be rejected
-   wildcard uri rules: 
	- example: `https://*.confirmit.com/*/logincallback`. 
	- Input must specify any value in the subdomain and path fraction to be accepted. 
	- Some example inputs:
	    -   `https://login.confirmit.com/app/logincallback` -  **accepted**
	    -   `https://auth.confirmit.com/login/logincallback` -  **accepted**
	    -   `https://app.confirmit.com/auth/folder/logincallback` -  **accepted**
	    -   `https://confirmit.com/logincallback` -  **NOT accepted**  (no subdomain present)
-   literal regex uri rules: 
	- example: `@https://([\w-]+\.)*confirmit\.com\/logincallback`. 
	- Input has to match the regex rule as literally specified. This format has to start with the character "@" and contain literal regex expression with escaped special characters
	- Some example inputs:
	    -   `https://login.confirmit.com/app/logincallback` -  **accepted**
	    -   `https://auth.confirmit.com/login/logincallback` -  **accepted**
	    -   `https://app.confirmit.com/auth/folder/logincallback` -  **accepted**
	    -   `https://confirmit.com/logincallback` -  **accepted**
	    -   `https://www.confirmit.org/logincallback` -  **NOT accepted**
    -   Characters that should be escaped: . \ + * ? [ ^ ] $ ( ) { } = ! < > | : -

Given these examples, a client should be able to make calls like this:
```
            string[] validationRules = new[]{
    "https://www.confirmit.com/webapp/logincallback", // absolute rule - this url is allowed
	"https://*.confirmit.com/*/logincallback", // wildcard rule
	"https://*.confirmit.com/webapp/login", // wildcard rule
	"@https://([\w-]+\.)*confirmit\.net\/logincallback" // literal regex rule	
};

            var validator = new UriValidator();
            var isValid = validator.Validate("https://www.confirmit.com/webapp/logincallback", validationRules); // expected true
            isValid = validator.Validate("https://www.confirmit.com/webapp/login", validationRules); // expected true
            isValid = validator.Validate("https://www.confirmit.net/webapp/login", validationRules); // expected false
```

## Challenge 2

Project `Api` in the solution contains two controllers. One is unsecured, the other one is secured. Open `ApiTests` test class and complete the two tests so they run and pass as specified in the comments.

### Challenge tasks
1. Use the provided solution in this folder as a starting point
2. Create a Git repository
3. Create a feature branch for your work
4. Create as many tests as you can that you consider useful. **Do a commit after each test.** Run tests and verify they fail for the right reasons.
	>Note that at this point many of these tests are expected to fail as there is no implementation yet.
5. Merge your code to master branch
6. **Optional**: Create a new feature branch and implement the Validate method.

That's it - best of luck!
