

# Singulink.Cryptography.PasswordChecker

[![Chat on Discord](https://img.shields.io/discord/906246067773923490)](https://discord.gg/EkQhJFsBu6)
[![View nuget packages](https://img.shields.io/nuget/v/Singulink.Cryptography.PasswordChecker.svg)](https://www.nuget.org/packages/Singulink.Cryptography.PasswordChecker/)
[![Build and Test](https://github.com/Singulink/Singulink.Cryptography.PasswordChecker/workflows/build%20and%20test/badge.svg)](https://github.com/Singulink/Singulink.Cryptography.PasswordChecker/actions?query=workflow%3A%22build+and+test%22)

**Singulink.Cryptography.PasswordChecker** uses algorithms similar to a dictionary attack password generator but in reverse - instead of generating passwords, it using matching rules to check if the password follows a predictable pattern with commonly used and contextual password words. This makes it easy to detect passwords that are vulnerable to simple dictionary attacks and follow the latest NIST password guidelines, giving your users lots of flexibility when choosing their passwords while still protecting them (without having to generate and maintain an actual password dictionary).

### About Singulink

We are a small team of engineers and designers dedicated to building beautiful, functional, and well-engineered software solutions. We offer very competitive rates as well as fixed-price contracts and welcome inquiries to discuss any custom development / project support needs you may have.

This package is part of our **Singulink Libraries** collection. Visit https://github.com/Singulink to see our full list of publicly available libraries and other open-source projects.

## 🚧 **UNDER DEVERLOPMENT 🚧** 

Project is currently in beta and still needs to be properly documented but you are welcome to use it as you see fit.

## Installation

The package is available on NuGet - simply install the `Singulink.Cryptography.PasswordChecker` package.

**Supported Runtimes**: Everywhere .NET Standard 2.0 is supported, including:
- .NET
- .NET Framework
- Mono / Xamarin

End-of-life runtime versions that are no longer officially supported are not tested or supported by this library.

## Usage

More information is coming soon, but for now you can have a [look at the tests](https://github.com/Singulink/Singulink.Cryptography.PasswordChecker/blob/main/Tests/Singulink.Cryptography.PasswordChecker.Tests/PasswordCheckerTests.cs) to get an idea of how it works.

The default [`PasswordMatchersProvider`](https://github.com/Singulink/Singulink.Cryptography.PasswordChecker/blob/main/Source/Singulink.Cryptography.PasswordChecker/PasswordMatchersProvider.cs) implementation returns a set of matchers that cover the most easily dictionary attacked password patterns using curated common password data in [`CommonMatchers`](https://github.com/Singulink/Singulink.Cryptography.PasswordChecker/blob/main/Source/Singulink.Cryptography.PasswordChecker/PasswordMatchers/CommonMatchers.cs) and contextual subjects you provide (i.e. the name of your service, the user's name / email address / etc). It matches with the top ~100 most common password words used in an easily predictable order.

If the password checker returns a match, you should display a message to the user something along the lines of `"Password matches our dictionary of contextual or top 100 common word variations in a predictable order. Please add a word or two (uncommon words are better) to your password to make it less predictable and succeptible to attacks"`. You can optionally display the list of matched words to the user so they can see the basic phrase their variation matched to.

The library is written to be extensible, so you can easily add your own matchers or override the default ones if you want to customize the behavior.

## Further Reading

API to be documented soon...

<!--
You can view the fully documented API on the [project documentation site](https://www.singulink.com/Docs/Singulink.Cryptography.PasswordChecker/api/Singulink.Cryptography.PasswordChecker.html).
-->

