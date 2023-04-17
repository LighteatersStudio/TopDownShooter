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
* <i>"content/[content name]"</i> - for content: like a music tracks, video clips etc


Jira implementation:
* Use pattern for <i>[feature name]</i>: <i>"[Jira task number]_[semantic name]"</i>. Example: <i>"feature/GP-149_LevelTimer"</i>
* If jira ticket not exist for your branch use: <i>[semantic name]</i>. Example: <i>"feature/LevelTimer"</i>


Commit comments:
* For each commit, a conscious comment
* When installing a plugin in a project, write the name of the plugin version in the comments


<b>Code Review</b>

<i>Pull request = PR</i>

Jira implementation:
* If PR finished task, use pattern for PR name: <i>"Task completed:[task number]. [semantic name]"</i>
* If PR finished task, add to description link to jira ticket(or few link to closed tickets)

PR example:https://github.com/LighteatersStudio/TopDownShooter/pull/19

WIP Pull Request:
* For not finished work use prefix <i>"[WIP]"</i> for PR name
* This type of PR is used to share current work, to get comments from colleagues or to show the progress of your decision
* This type of PR cannot be approved. But it can be used to discuss the solution in the comments
* After completing work on the task, you need to remove the [WIP] prefix from the name. After that, the PR can be approved and merged
* You do not need to add the [WIP] prefix to the name of the working branch.
