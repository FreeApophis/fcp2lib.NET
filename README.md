# fcp2lib.NET

This is a complete implementation of the FCP2 (Freenet Client Protocol 2.0) specification for the access of Freenet-nodes with any .NET Language.

[![Build Status](https://travis-ci.org/FreeApophis/fcp2lib.NET.svg?branch=master)](https://travis-ci.org/FreeApophis/fcp2lib.NET)
[![NuGet package](https://buildstats.info/nuget/fcp2lib.NET)](https://www.nuget.org/packages/fcp2lib.NET)

It is written in C# and tries to parse all FCP message-types of the Freenet node and has an Event-driven Interface. 

## Features

* Event Driven Design : Each Message gets a certain Event in the Library
* Simple and Complex Methods: There is always a Method with only the Mandatory Parameters, and one with all.
* Complete Implementation of the FCP 2.0 Protocol
* Completely Typed access to all the Variables in the messages.
* Completely Non-Blocking Design
* Fully dynamic core (easily extensible to new messages)
* Implemented with C# 5.0

## License

GNU General Public License 3.0 (Other licenses negotiable)
