var mailCtrl = function ($scope) {
    $scope.Test = "Mail Service Providers";
}

var sendCtrl = function ($scope) {
    $scope.Test = "Mail Drafting";

    $scope.SendEmail = function (APIServiceFactory) {
        console.log(APIServiceFactory);
    if ($scope.to != undefined) {
        emailObject = {
            "from": $scope.from,
            "to": $scope.to,
            "cc": $scope.cc,
            "bcc": $scope.bcc,
            "subject": $scope.subject,
            "text": $scope.text          
        }
        if (emailObject != null) {
            var result = APIServiceFactory.getAPICallsWithModel("SendEmail", emailObject)
                .then(function (response) {
                    console.log("Data received at the client" + response.data);
                    if (response.status == 200) {
                        $scope.ReturnValue = response.data;                        
                        $scope.showError = false;

                    }
                    else {
                        $scope.showError = true;                        
                    }
                });

        }
    }
    else {
        alert("Enter atleast main Recipient email");
        return;
    }
}
}

