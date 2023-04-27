# TopDownShooter
It's a game.


<b>Code Convention</b>

We use standard Microsoft code style convention: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions



<b>Git Convention</b>

Git strategy
* For any changes we use separate branches. Branch reuse is allowed.
* After the changes are complete it's necessary to merge the working branch into the <b>develop</b> branch. To do this you need to use pull requests.
* Committing changes directly to the <b>develop</b> or <b>main</b> branches isn't allowed.

Branch naming:
* <i>"feature/[feature name]"</i> - for feature development.
* <i>"fix/[fix name]"</i> - for bug fixing.
* <i>"plugin/[plugin name]"</i> - for plugin installation.
* <i>"content/[content name]"</i> - for content like music tracks, video clips etc.


Jira implementation:
* Use the following pattern for <i>[feature name]</i>: <i>"[Jira task number]_[semantic name]"</i>. Example: <i>"feature/GP-149_LevelTimer"</i>.
* If jira ticket doesn't exist for your branch than use: <i>"[semantic name]"</i>. Example: <i>"feature/LevelTimer"</i>.
* The same rules applies to bug fixing branches.


Commit comments:
* For each commit use a conscious comment.
* When installing a plugin into the project, write the plugin version in the comments.


<b>Code Review</b>

<i>Pull request = PR</i>.

Jira implementation:
* For completed PR tasks use the following PR name pattern: <i>"Task completed:[task number]. [semantic name]"</i>.
* For completed PR tasks add a link or a few links to a jira ticket(-s) to the description.

PR example: https://github.com/LighteatersStudio/TopDownShooter/pull/19

WIP Pull Request:
* For unfinished work use prefix <i>"[WIP]"</i> in PR name. 
* This type of PR is used to share current work, to get comments from colleagues or to show the progress of your decision.
* This type of PR can't be approved but can be used to discuss the solution in the comments.
* Once this task is complete you need to remove the <i>"[WIP]"</i> prefix from PR name. After that it can be approved and merged.
* You shouldn't add the <i>"[WIP]"</i> prefix to the working branch name.

Sprite format:
* Only PNG or JPG sprite formats are allowed in the project. This rule doesn't apply to uncompressed textures.
