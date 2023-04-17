# TopDownShooter
It's game


<b>Code Convention</b>

We use standart microsoft code style convention: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions



<b>Git Convention</b>

Git strategy
* For any changes, we use a separate branch. Branch reuse is allowed
* After the changes are brought to the end, it is necessary to merge the working branch into the <b>develop</b> branch. To do this, you need to use pull requests
* Committing changes directly to the <b>develop</b> or <b>main</b> branches is not allowed

Branch naming:
* <i>"feature/[feature name]"</i> - for feature development
* <i>"fix/[fix name]"</i> - for bug fixing
* <i>"plugin/[plugin name]"</i> - for plugin installation

Jira implementation:
* Use pattern for <i>[feature name]</i>: <i>"[Jira task number]_[semantic name]"</i>. Example: <i>"feature/GP-149_LevelTimer"</i>
* If jira ticket not exist for your branch use: <i>[semantic name]</i>. Example: <i>"feature/LevelTimer"</i>


Commit comments:
* For each commit, a conscious comment
* When installing a plugin in a project, write the name of the plugin version in the comments


<b>Code Review</b>

Jira implementation:
* If PR finished task, use pattern for Pull Request name: <i>"Task completed:[task number]. [semantic name]"</i>
* If PR finished task, add to comments link to jira ticket(or few link to closed tickets)

PR example:https://github.com/LighteatersStudio/TopDownShooter/pull/19
