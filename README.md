# dotcache
C# DotNet In-Memory Reverse Proxy Cache


## Todo
* [] Identify Feature set and requirements
* [] MVP Setup Reverse Proxy with simple logging middleware first
* [] Implement cache eviction policies (e.g., LRU, LFU, TTL)
* [] Implement checking for cache control headers (e.g., Cache-Control, Expires) to determine cacheability of responses
* [] Implement checking for X-DotCache headers to control caching behaviour
	* X-DotCache-IgnoreParameters CSV List or WildCard
	* X-DotCache-IgnoreHeaders CSV List or WildCard
	* X-DotCache-Cache Boolean
* [] Reverse Proxy using YARP
* [] Orchestrator
* [] Metrics and monitoring
