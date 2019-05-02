//uses the AWS SDK
var bucketName = 'classtalknotes';
var bucketRegion = 'eu-west-1';
var IdentityPoolId = 'eu-west-1:4b6476b1-7eb8-4376-a6f6-5ab2f3bfbff5';
const bucketUrl = `https://s3-eu-west-1.amazonaws.com//${bucketName}`;



//using AWS SDK
AWS.config.update({
  region: bucketRegion,
  credentials: new AWS.CognitoIdentityCredentials({
    IdentityPoolId: IdentityPoolId
  })
});
const s3 = new AWS.S3({
  apiVersion: '2006-03-01',
  params: {Bucket: bucketName}
  
});


function addFile() {
  const files = document.getElementById('upload').files;
  const file = files[0];
  const fileName = 'vrnotes.txt';//to ensure the file name is always the same

  //build the params needed for the putObject call
  const params = {
	Bucket: bucketName,
    Key: fileName,
    Body: file,
    ACL: 'public-read' //this makes the object readable 
  }

  s3.putObject(params, function(err, data) {
    if (err) {
		alert('Something went wrong. Check file format');
      if (err) console.log(err, err.stack);
    } else {
      //if successful
	  alert('You have successfully uploaded your notes.');
      
    }
  });
}


