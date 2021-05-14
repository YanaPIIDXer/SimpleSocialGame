require 'rails_helper'

RSpec.describe "Expansions", type: :request do
  describe "GET /index" do
    it "returns http success" do
      get "/expansions"
      expect(response).to have_http_status(:success)
    end
  end
end
