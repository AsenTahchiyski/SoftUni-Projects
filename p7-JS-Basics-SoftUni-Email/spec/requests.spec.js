var request = require("request");
var base_url = "http://localhost:3000/"

describe("A suite", function() {
	it("home request returns status code 200", function(done) {
		request.get(base_url, function(error, response, body) {
			expect(response.statusCode).toBe(200);
			done();
		});
	});
	
	it("users request returns status code 200", function(done) {
		request.get(base_url + "users", function(error, response, body) {
			expect(response.statusCode).toBe(200);
			done();
		});
	});
	
	it("random request returns status code 404", function(done) {
		request.get(base_url + "random123", function(error, response, body) {
			expect(response.statusCode).toBe(404);
			done();
		});
	});
});